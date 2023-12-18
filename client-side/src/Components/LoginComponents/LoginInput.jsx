import React, {useState} from 'react';
import axios from "axios";
import {handleChangeInput} from "../../Logic/HandlingChangeInput";

const LoginInput = () => {
    const [formData, setFormData] = useState({
        Login: '', Password: ''
    });
    const URL = process.env.REACT_APP_URL

    const formFields = [{
        label: "Email", key: "Login"
    }, {
        label: "Hasło", key: "Password"
    }];

    const HandleLogin = async (e) => {
        e.preventDefault();
        try {
            await axios.post(URL + "/login-auth", formData);
            //todo: when user login is success then get token and set it with use context
        } catch (error) {
            console.error('Error:', error.message);
        }
    };


    return (<div className='border'>
        {formFields.map(field => (<div key={field.key}>
            <label>{field.label}</label>
            <input
                type="text"
                className='flex flex-col border'
                value={formData[field.key]}
                onChange={(e) => handleChangeInput(setFormData, formData, e, field.key)}
            />
        </div>))}
        <div className='text-center'>
            <button type="submit" onClick={HandleLogin}>Zaloguj</button>
        </div>
    </div>);
};

export default LoginInput;
