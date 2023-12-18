import React, {createContext, useState} from "react";

const defaultSettings = {
    token: "",
    tokenSetter: {},
}
export const AuthContext = createContext(defaultSettings)

export const AuthContextProvider = (props) => {
    const [token, setToken] = useState("test"); //initialState test for accessing the protected routes
    const [isLoading, setIsLoading] = useState(true)

    const tokenSetter = async (token) => {
        setIsLoading(true)
        const res = await setToken(token)
        if (res) {
            setIsLoading(false)
        }
    }
    return <AuthContext.Provider value={{token, tokenSetter, isLoading}}>{props.children}</AuthContext.Provider>
}