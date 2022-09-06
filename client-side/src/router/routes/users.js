export default [
  {
    path: "/register",
    name: "Register",
    component: () =>
      import(
        /* webpackChunkName: "register" */ "../../views/Authorization/Register/Register.vue"
      ),
  },
  {
    path: "/login",
    name: "Login",
    component: () =>
      import(
        /* webpackChunkName: "login" */ "../../views/Authorization/Login/Login.vue"
      ),
  },
];
