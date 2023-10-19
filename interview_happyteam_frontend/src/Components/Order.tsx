import { useEffect, useState } from "react";
import { Alert, Col, Form, Row } from "react-bootstrap";
import Button from "react-bootstrap/Button";
import "../Styles/Order.css";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useAPI } from "../Core/Context/APIContext";
import { IOrder, IError } from "../Core/Interfaces/Interfaces";

const Order = () => {
  const { carConfig, countryConfig, LogError, CreateOrder } = useAPI();

  const [show, setShow] = useState(false);
  const [alerMessage, setAlertMessage] = useState<{
    result: boolean;
    message: string;
  }>({ result: false, message: "" });
  const [selectedDateStart, setSelectedDateStart] = useState(null);
  const [selectedDateEnd, setSelectedDateEnd] = useState(null);
  const [totalPrice, setTotalPrice] = useState<number>(0);

  const [formData, setFormData] = useState<IOrder>({});
  const [formErrors, setFormErrors] = useState<IError>({});

  const handleInputChange = (e: any) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleDateStartChange = (date: any) => {
    setSelectedDateStart(date);
    setFormData({
      ...formData,
      StartDate: date,
    });
  };

  const handleDateEndChange = (date: any) => {
    setSelectedDateEnd(date);
    setFormData({
      ...formData,
      EndDate: date,
      Country: "Spain" // MOCK
    });
  };

  useEffect(() => {
    if (formData?.Car && formData?.StartDate && formData?.EndDate) {
      const differenceTime =
        formData.EndDate.getTime() - formData.StartDate.getTime();
      const differenceInDays = Math.floor(
        differenceTime / (1000 * 60 * 60 * 24)
      );

      let costPerDay = 0;

      carConfig?.Cars.forEach((car) => {
        if (car.Model === formData.Car) {
          costPerDay = car.PricePerDay;
        }
      });

      setTotalPrice(
        differenceInDays > 1 ? differenceInDays * costPerDay : costPerDay
      );
    }
  }, [formData?.Car, formData?.StartDate, formData?.EndDate]);

  const handleSubmit = (e: any) => {
    e.preventDefault();
    const errors: IError = {};

    if (!formData?.Car) {
      errors.carModel = "Car Model is required";
    }

    if (!formData?.LocationStart) {
      errors.locationStart = "Location Start is required";
    }

    if (!formData?.LocationEnd) {
      errors.locationEnd = "Location End is required";
    }

    if (!formData?.StartDate) {
      errors.dateStart = "Date Start is required";
    }

    if (!formData?.EndDate) {
      errors.dateEnd = "Date End is required";
    }

    if (
      formData?.EndDate &&
      formData?.StartDate &&
      formData.StartDate > formData.EndDate
    ) {
      errors.validDate = "End Date cannot be earlier that Start Date";
    }

    setFormErrors(errors);
    if (Object.keys(errors).length === 0) {      
      MakeOrder(formData);
    }
  };

  const MakeOrder = (data: IOrder) => {
    const callAPI = async (data: IOrder) => {
      const result = await CreateOrder!(data);
      console.log("EHHRERE")
      setAlertMessage(result);
      setShow(true);
    };
    callAPI(data).then();
  };

  return (
    <>
      <Form className="OrderForm" onSubmit={handleSubmit}>
        <Form.Group as={Row} className="mb-3" controlId="carModel">
          <Form.Label column sm={2}>
            Car Model
          </Form.Label>
          <Col sm={10}>
            <Form.Select
              name="Car"
              onChange={handleInputChange}
              value={formData?.Car ?? ""}
            >
              <option value="">Select a car model</option>
              {carConfig?.Cars.map((car) => (
                <option key={car.Id} value={car.Model}>
                  {car.Model}
                </option>
              ))}
            </Form.Select>
            {formErrors?.carModel && (
              <div className="text-danger">{formErrors.carModel}</div>
            )}
          </Col>
        </Form.Group>
        <Form.Group as={Row} className="mb-3" controlId="locationStart">
          <Form.Label column sm={2}>
            Location Start
          </Form.Label>
          <Col sm={10}>
            <Form.Select
              name="LocationStart"
              onChange={handleInputChange}
              value={formData?.LocationStart ?? ""}
            >
              <option value="">Select a start location</option>
              {countryConfig?.Countries.filter((country) => country.Name == "Spain")[0].Locations?.map((location) => (
                <option key={location.Id} value={location.Name}>
                  {location.Name}
                </option>
              ))}
            </Form.Select>
            {formErrors?.locationStart && (
              <div className="text-danger">{formErrors.locationStart}</div>
            )}
          </Col>
        </Form.Group>
        <Form.Group as={Row} className="mb-3" controlId="locationEnd">
          <Form.Label column sm={2}>
            Location End
          </Form.Label>
          <Col sm={10}>
            <Form.Select
              name="LocationEnd"
              onChange={handleInputChange}
              value={formData?.LocationEnd ?? ""}
            >
              <option value="">Select a end location</option>
              {countryConfig?.Countries.filter((country) => country.Name == "Spain")[0].Locations?.map((location) => (
                <option key={location.Id} value={location.Name}>
                  {location.Name}
                </option>
              ))}
            </Form.Select>
            {formErrors?.locationEnd && (
              <div className="text-danger">{formErrors.locationEnd}</div>
            )}
          </Col>
        </Form.Group>
        <Form.Group as={Row} className="mb-3" controlId="dateStart">
          <Form.Label column sm={2}>
            Date Start
          </Form.Label>
          <Col sm={10}>
            <DatePicker
              selected={selectedDateStart}
              onChange={handleDateStartChange}
              dateFormat="dd/MM/yyyy"
              isClearable
            />
            {formErrors?.dateStart && (
              <div className="text-danger">{formErrors.dateStart}</div>
            )}
          </Col>
        </Form.Group>
        <Form.Group as={Row} className="mb-3" controlId="dateEnd">
          <Form.Label column sm={2}>
            Date End
          </Form.Label>
          <Col sm={10}>
            <DatePicker
              selected={selectedDateEnd}
              onChange={handleDateEndChange}
              dateFormat="dd/MM/yyyy"
              isClearable
            />
            {formErrors?.dateEnd && (
              <div className="text-danger">{formErrors.dateEnd}</div>
            )}
          </Col>
          {formErrors?.validDate && (
            <div className="text-danger">{formErrors.validDate}</div>
          )}
        </Form.Group>
        {totalPrice > 0 && (
            <div className="text-warning">You will have to pay: {totalPrice} $</div>
          )}
        <Col sm={3}>
          <Button type="submit">Make an Order</Button>
        </Col>
      </Form>
      <Alert show={show} variant={alerMessage.result ? "success" : "danger"}>
        <Alert.Heading>Alert</Alert.Heading>
        <p>{alerMessage.message}</p>
        <hr />
        <div className="d-flex justify-content-end">
          <Button
            onClick={() => setShow(false)}
            variant={alerMessage.result ? "outline-success" : "outline-danger"}
          >
            Close me
          </Button>
        </div>
      </Alert>
    </>
  );
};

export default Order;
