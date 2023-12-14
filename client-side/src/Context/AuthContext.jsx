import {createContext, useState} from "react";

const defaultSettings = {
    token:"",
    tokenModifyer:(token) => {}
}
export const AuthContext = createContext(defaultSettings)

export const AuthContextProvider = (props) => {
    const [token, setToken] = useState("");
    const tokenModifyer = (token) => {setToken(token)}

    return <AuthContext.Provider value={{token, tokenModifyer}}>{props.children}</AuthContext.Provider>
}