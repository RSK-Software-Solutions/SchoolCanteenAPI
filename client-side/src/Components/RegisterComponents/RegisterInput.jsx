import React, {useContext, useState} from 'react';
import axios from 'axios';
import {AuthContext} from "../../Context/AuthContext";
import {Navigate} from "react-router-dom";
import {handleChangeInput} from "../../Logic/HandlingChangeInput";

const RegisterInput = () => {
    const [formData, setFormData] = useState({
        Name: '',
        Login: '',
        Password: ''
    });

    const URL = process.env.REACT_APP_URL;

    const data = useContext(AuthContext)
    const token = data.token;

    const formFields = [
        {label: 'Firma', key: 'Name'},
        {label: 'Login', key: 'Login'},
        {label: 'Hasło', key: 'Password'}
    ];

    const isExistingCompany = async () => {
        try {
            const companies = await axios.get(URL + "/GetAll");
            var exitingCompany = companies.data.filter(Firm => Firm.name === formData.Name)
            return exitingCompany.length > 0;

        } catch (err) {
            console.error(err)
        }
    }
    const HandleRegisterCompany = async (e) => {
        e.preventDefault();
        try {
            if (await isExistingCompany()) {
                return
            }

            if (!formData.Name) return
            if (!formData.Login) return
            if (!formData.Password) return

            await axios.post(URL, formData);
            data.tokenModifyer("here token from backend") // setting token to have access through every component
            setFormData({
                Name: "",
                Password: "",
                Login: "",
            })
            localStorage.setItem("userSession", token) //setting localstorage to have token if token is missing then user will log out
            return <Navigate to={'/dashboard'}/> //registered user is navigated to app

        } catch (error) {
            return error;
        }
    };

    return (
        <div className='border'>
            {formFields.map((field) => (
                <div key={field.key}>
                    <label>{field.label}</label>
                    <input
                        type="text"
                        className='flex flex-col border'
                        value={formData[field.key]}
                        onChange={(e) => handleChangeInput(setFormData, formData, e, field.key)}
                    />
                </div>
            ))}
            <div className='text-center'>
                <button type="button" onClick={HandleRegisterCompany}>
                    Zarejestruj
                </button>
            </div>
        </div>
    );
};

export default RegisterInput;
