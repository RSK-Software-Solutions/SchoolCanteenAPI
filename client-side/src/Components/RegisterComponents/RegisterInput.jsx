import React, {useState} from 'react';
import axios from 'axios';

const RegisterInput = () => {
    const [formData, setFormData] = useState({
        Name: '',
        Login: '',
        Password: ''
    });
    const [message, setMessage] = useState("Status Rejestracji:")

    const URL = process.env.REACT_APP_URL;

    const formFields = [
        {label: 'Firma', key: 'Name'},
        {label: 'Login', key: 'Login'},
        {label: 'Hasło', key: 'Password'}
    ];

    const handleChange = (e, field) => {
        setFormData({
            ...formData,
            [field]: e.target.value
        });
    };

    const isExistingCompany = async () => {
        try {
            const companies = await axios.get(URL);
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
                return setMessage("Firma już istnieję.")
            }

            if (!formData.Name) return setMessage("Proszę wpisać nazwę Firmy")
            if (!formData.Login) return setMessage("Proszę wpisać Login")
            if (!formData.Password) return setMessage("Proszę wpisać Hasło")

            await axios.post(URL, formData);
            setMessage("")
            setFormData({
                Name: "",
                Password: "",
                Login: "",
            })

        } catch (error) {
            setMessage("Error, Please try later.")
        }
    };

    return (
        <div>
            {formFields.map((field) => (
                <div key={field.key} className="mt-3 mx-3">
                    <label>{field.label}</label>
                    <input
                        type="text"
                        className="input-credentials flex"
                        value={formData[field.key]}
                        onChange={(e) => handleChange(e, field.key)}
                    />
                </div>
            ))}
                {message ? (<div className="error-message flex pt-3">{message}</div>) : null}
            <div className="flex justify-center">
                <button type="button" className="btn" onClick={HandleRegisterCompany}>
                    Zarejestruj
                </button>
            </div>
        </div>
    );
};

export default RegisterInput;
