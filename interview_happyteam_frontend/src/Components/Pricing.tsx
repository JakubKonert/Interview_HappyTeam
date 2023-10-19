import { useState } from "react";
import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import { useAPI } from "../Core/Context/APIContext";
import { useNavigate } from "react-router-dom";
import { ListGroup } from "react-bootstrap";

const Pricing = () => {
  const { carConfig } = useAPI();

  const navigator = useNavigate();

  return (
    <>
      {carConfig?.Cars.map((car) => {
        const src = `/Images/${car.Model}.jpg`;
        return (
          <Card style={{marginTop: 20, marginBottom: 80, padding:20}}>
            <Card.Img variant="top" src={src} />
            <Card.Body>
              <Card.Title>{car.Model}</Card.Title>
              <Card.Text>
                <ListGroup>
                  <ListGroup.Item>Price per day: {car.PricePerDay}</ListGroup.Item>
                  <ListGroup.Item>Available amount: {car.Amount}</ListGroup.Item>
                </ListGroup>
              </Card.Text>
            </Card.Body>
            <Card.Footer>
              <Button
                variant={car.IsAvailable ? "success" : "danger"}
                disabled={!car.IsAvailable}
                onClick={() => navigator("/order")}
              >
                Make an order
              </Button>
            </Card.Footer>
          </Card>
        );
      })}
    </>
  );
};

export default Pricing;
