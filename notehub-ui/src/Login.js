import './Login.css';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import Alert from 'react-bootstrap/Alert';
import { Link, useLocation } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useEffect } from 'react';

function Login() {
    var query = new URLSearchParams(useLocation().search);
    var qlogout = query.get("logout");
    //parametre olarak verilen metot Login bileşenini sayfada render/update olunce çalışır
    useEffect(() => {
        if (qlogout == "success") {
            toast("You have logged out succesfully!");
        }
    });

    return (
        <Card className="card-login">
            <Card.Body className="p-sm-4">
                <ToastContainer></ToastContainer>
                <h1 className="text-center">Login</h1>
                <Alert variant="danger">
                    E-mail already exists.
                    </Alert>
                <Form>
                    <Form.Group controlId="formBasicEmail">
                        <Form.Label>Email address</Form.Label>
                        <Form.Control type="email" placeholder="Enter email" />
                    </Form.Group>

                    <Form.Group controlId="formBasicPassword">
                        <Form.Label>Password</Form.Label>
                        <Form.Control type="password" placeholder="Password" />
                    </Form.Group>

                    <Form.Group controlId="formBasicCheckbox">
                        <Form.Check type="checkbox" label="Remember me" />
                    </Form.Group>

                    <Button variant="primary" type="submit">
                        Submit
                </Button>
                </Form>
                <div className="text-center mt-3">
                    <Link to="/register">Register as a new user</Link>
                </div>
            </Card.Body>
        </Card>
    )
}

export default Login;
