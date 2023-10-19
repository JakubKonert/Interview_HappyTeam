import { useState } from "react";
import Carousel from "react-bootstrap/Carousel";
import Image from "react-bootstrap/Image";
import "../Styles/Image.css";
import { useAPI } from "../Core/Context/APIContext";
import { useNavigate } from "react-router-dom";

const Home = () => {
  const { carConfig} = useAPI();
  const [index, setIndex] = useState<number>(0);

  const navigator = useNavigate();

  const handleSelect = (selectedIndex: number) => {
    setIndex(selectedIndex);
  };
  return (
    <>
      <Carousel activeIndex={index} onSelect={handleSelect}>
        {carConfig?.Cars.map((car) => {
          const src = `/Images/${car.Model}.jpg`;
          return (
            <Carousel.Item onClick={() => navigator("/order")} >
              <Image src={src} className="d-block w-100" />
              <Carousel.Caption>
                <h3>{car.Model}</h3>
              </Carousel.Caption>
            </Carousel.Item>
          );
        })}
      </Carousel>
    </>
  );
};

export default Home;
