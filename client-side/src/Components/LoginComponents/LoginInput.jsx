import React, {useState} from 'react';
const RegisterInput = () => {
    const [formData, setFormData] = useState({
        Login: '', Password: ''
    });
    const URL = process.env.REACT_APP_URL

    const formFields = [{
        label: "Email", key: "Login"
    }, {
        label: "Hasło", key: "Password"
    }];

    const handleChange = (e, field) => {
        setFormData({
            ...formData, [field]: e.target.value
        });
    };

    const handleSubmit = async (e) => {
        try {
            e.preventDefault();
            console.log(formData)
            const response = await fetch(URL, {
                method: "POST",
                body: JSON.stringify(formData),
                headers: {"Content-Type": "application/json"}
            })
            let jsonData = response.json();

            console.log(jsonData);
        } catch (error) {

            console.error('Error:', error.message);
        }
    };


    return (<div>
        {formFields.map(field => (<div key={field.key} className='mt-3 mx-3'>
            <label>{field.label}</label>
            <input
                type="text"
                className='input-credentials flex flex-col'
                value={formData[field.key]}
                onChange={(e) => handleChange(e, field.key)}
            />
        </div>))}
        <div className='flex justify-center'>
            <button type="submit" className='btn my-5 shadow-md' onClick={handleSubmit}>Zaloguj</button>
        </div>
    </div>);
};

export default RegisterInput;
