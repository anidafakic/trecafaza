package model

type Reservation struct {
	ID           string `json:"id,omitempty" bson:"_id,omitempty"`
	GuestID      string `json:"guestId" bson:"guestId"`
	RoomID       string `json:"roomId" bson:"roomId"`
	CheckInDate  string `json:"checkInDate" bson:"checkInDate"`
	CheckOutDate string `json:"checkOutDate" bson:"checkOutDate"`
	Status       string `json:"status" bson:"status"`
}
