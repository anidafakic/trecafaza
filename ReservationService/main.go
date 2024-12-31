package main

import (
	"ReservationService/controllers"
	"fmt"
	"log"
	"net/http"

	"github.com/gin-gonic/gin"
	"github.com/joho/godotenv"
	healthcheck "github.com/tavsec/gin-healthcheck"
	"github.com/tavsec/gin-healthcheck/checks"
	"github.com/tavsec/gin-healthcheck/config"
)

func main() {
	err := godotenv.Load()
	if err != nil {
		log.Fatal("Error loading .env file")
	}
	fmt.Println("Server is getting started...")

	router := gin.Default()

	router.POST("/reservation", controllers.CreateReservation)
	router.GET("/reservation/:id", controllers.GetReservationByID)
	router.GET("/reservations", controllers.GetAllReservations)
	router.PUT("/reservation/:id", controllers.UpdateReservationByID)
	router.DELETE("/reservation/:id", controllers.DeleteReservationByID)

	healthcheck.New(router, config.DefaultConfig(), []checks.Check{})

	log.Fatal(http.ListenAndServe(":8080", router))
	fmt.Println("Listening on port 8080")
}
