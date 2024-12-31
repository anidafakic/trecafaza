package controllers

import (
	"ReservationService/model"
	"context"
	"fmt"
	"log"
	"net/http"
	"os"

	"github.com/gin-gonic/gin"
	"github.com/joho/godotenv"
	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

var reservationCollection *mongo.Collection

// Inicijalizacija konekcije sa MongoDB-om
func init() {
	err := godotenv.Load(".env")
	if err != nil {
		log.Fatalf("Greška prilikom učitavanja .env datoteke: %v", err)
	}

	connectionString := os.Getenv("MONGODB_CONNECTION_STRING")
	clientOptions := options.Client().ApplyURI(connectionString)

	client, error := mongo.Connect(context.TODO(), clientOptions)
	if error != nil {
		log.Fatalf("Greška pri povezivanju sa MongoDB-om: %v", error)
	}

	fmt.Println("MongoDB connection success")

	dbName := os.Getenv("DBNAME")
	colName := os.Getenv("COLNAME")

	reservationCollection = client.Database(dbName).Collection(colName)

	fmt.Println("Collection instance is ready")
}

// Kreira novu rezervaciju
func CreateReservation(c *gin.Context) {
	var res model.Reservation

	// Parsira JSON sa frontend-a u strukturu modela rezervacije
	if err := c.ShouldBindJSON(&res); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	// Ubacivanje u bazu
	result, err := reservationCollection.InsertOne(context.Background(), res)
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	c.JSON(http.StatusOK, gin.H{"insertedID": result.InsertedID})
}

func GetReservationByID(c *gin.Context) {
	resID := c.Param("id")

	var res model.Reservation
	err := reservationCollection.FindOne(context.Background(), bson.M{"_id": resID}).Decode(&res)
	if err != nil {
		c.JSON(http.StatusNotFound, gin.H{"error": "Reservation not found"})
		return
	}

	c.JSON(http.StatusOK, res)
}

func GetAllReservations(c *gin.Context) {
	cursor, err := reservationCollection.Find(context.Background(), bson.D{})
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	var reservations []model.Reservation
	if err := cursor.All(context.Background(), &reservations); err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	c.JSON(http.StatusOK, reservations)
}

func UpdateReservationByID(c *gin.Context) {
	resID := c.Param("id")
	var updateRes model.Reservation

	if err := c.ShouldBindJSON(&updateRes); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	result, err := reservationCollection.UpdateOne(context.Background(),
		bson.M{"_id": resID}, bson.M{"$set": updateRes})
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	if result.MatchedCount == 0 {
		c.JSON(http.StatusNotFound, gin.H{"error": "Reservation not found"})
		return
	}

	c.JSON(http.StatusOK, gin.H{"modifiedCount": result.ModifiedCount})
}

func DeleteReservationByID(c *gin.Context) {
	resID := c.Param("id")

	result, err := reservationCollection.DeleteOne(context.Background(), bson.M{"_id": resID})
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}

	if result.DeletedCount == 0 {
		c.JSON(http.StatusNotFound, gin.H{"error": "Reservation not found"})
		return
	}

	c.JSON(http.StatusOK, gin.H{"deletedCount": result.DeletedCount})
}
