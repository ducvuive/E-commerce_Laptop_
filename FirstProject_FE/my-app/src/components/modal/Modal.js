import React, { useState } from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";

function ConfirmAction(props) {
  return (
    <Modal {...props} aria-labelledby="contained-modal-title-vcenter">
      <Modal.Header closeButton>
        <Modal.Title id="contained-modal-title-vcenter">
          <h3>{props.title}</h3>
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <p>{props.content}</p>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={props.onHide}>
          Đóng
        </Button>
        <Button variant="primary" onClick={props.onConfirm}>
          Xác nhận
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

export default ConfirmAction;
