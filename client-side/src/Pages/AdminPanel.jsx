import React from 'react';

const AdminPanel = () => {
    return (
        <div>
            <div>
                Zarządzanie zespołem
                <input type="search" placeholder="Szukaj pracownika" />
                <button>Dodaj pracownika</button>
                <button>Usuń pracownika</button>
                <button>Edytuj pracownika</button>
            </div>


            <div>Zarządzanie finansami

                <p>INFORMACJE O WYDATKACH</p>
                <p>INFORMACJE O ZAROBKACH</p>
                <p>INFORMACJE O PRZYCHODACH</p>

            </div>
        </div>
    );
};

export default AdminPanel;
