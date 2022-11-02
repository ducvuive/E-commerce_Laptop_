import logo from "./logo.svg";
import NavigationRow from "./components/NavigationRow";
import "bootstrap/dist/css/bootstrap.min.css";
import { Fragment } from "react";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import "./App.css";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Dashboard from "./pages/Dashboard";
import ListProduct from "./pages/ListProduct";
import Categories from "./pages/Categories";
import SlideBar from "./components/SlideBar";
import NavBar from "./components/NavBar";
import DetailCategories from "./pages/Categories/DetailCategories";
import CreateCategories from "./pages/Categories/CreateCategories";
import UpdateCategories from "./pages/Categories/UpdateCategories";
function App() {
  return (
    // <Fragment className="vh-100">
    //   <Container fluid className="h-auto p-0">
    //     <Row className="">
    //       <Col xs={3} className="p-0">
    //         <NavigationRow />
    //       </Col>
    //       <Col xs={9} className="">
    //         2 of 3 (wider)
    //       </Col>
    //     </Row>
    //   </Container>
    // </Fragment>
    <BrowserRouter>
      <NavBar></NavBar>
      <SlideBar>
        <Routes>
          <Route path="/dashboard" element={<Dashboard />}></Route>
          <Route path="/listProduct" element={<ListProduct />}></Route>
          <Route path="/categories" element={<Categories />}></Route>
          <Route
            path="/categories/Detail/:id"
            element={<DetailCategories />}
          ></Route>
          <Route
            path="/categories/Update/:id"
            element={<UpdateCategories />}
          ></Route>
          <Route
            path="/categories/Create"
            element={<CreateCategories />}
          ></Route>
        </Routes>
      </SlideBar>
    </BrowserRouter>
  );
}

export default App;
