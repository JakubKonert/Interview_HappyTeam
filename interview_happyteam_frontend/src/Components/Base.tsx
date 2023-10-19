import { Row } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import { ReactNode } from "react";

type ChildrenType = {
  children: ReactNode;
};

const Base = ({ children }: ChildrenType) => {

  return (
    <>
      <Container>
        <Row>
          <Navbar bg="dark" data-bs-theme="dark" sticky="top">
            <Container>
              <Navbar.Brand href="/">TeslaFlex</Navbar.Brand>
              <Nav className="me-auto">           
                <Nav.Link href="home">Home</Nav.Link>
                <Nav.Link href="pricing">Pricing</Nav.Link>
                <Nav.Link href="order">Make Order</Nav.Link>
              </Nav>
              <Navbar.Collapse className="justify-content-end">
                <Navbar.Text>
                  Signed in as: <span className="text-light">Interviewer</span>
                </Navbar.Text>
              </Navbar.Collapse>
            </Container>
          </Navbar>
        </Row>
        <Row>{children}</Row>
        <Row>
          <div className="fixed-bottom bg-dark text-light">
            <p className="text-center">
              Happy Team Interview 2023 - Jakub Konert
            </p>
          </div>
        </Row>
      </Container>
    </>
  );
};

export default Base;
