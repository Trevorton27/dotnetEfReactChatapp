import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

const AuthorizedRoute = () => {
  const isLoggedIn = localStorage.getItem('isAuthenticated');

  return isLoggedIn !== null ? <Component /> : <Redirect to='/login' />;
};

export default AuthorizedRoute;
