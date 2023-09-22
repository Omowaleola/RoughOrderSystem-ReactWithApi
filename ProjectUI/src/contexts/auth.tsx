import React, { useState, useEffect, createContext, useContext, useCallback } from 'react';
import { getUser, signIn as sendSignInRequest, signOut as signOutRequest } from '../api/auth';
import type {  AuthContextType } from '../types';
import {CustomerLoggedInModel, CustomerViewModel} from "../models/customer-view-model";

function AuthProvider(props: React.PropsWithChildren<unknown>) {
  const [user, setUser] = useState<CustomerLoggedInModel>();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    (async function () {
      const result = await getUser();
      if (result.isOk) {
        setUser(result.data);
      }

      setLoading(false);
    })();
  }, []);

  const signIn = useCallback(async (email: string, password: string) => {
    const result = await sendSignInRequest(email, password);
    if (result.isOk) {
      setUser(result.data);
    }

    return result;
  }, []);

  const signOut = useCallback(() => {
    const result = signOutRequest();
    if(result.isOk){
      setUser(undefined);
    }
  }, []);


  return (
    <AuthContext.Provider value={{ user, signIn, signOut, loading }} {...props} />
  );
}

const AuthContext = createContext<AuthContextType>({ loading: false } as AuthContextType);
const useAuth = () => useContext(AuthContext);

export { AuthProvider, useAuth }
