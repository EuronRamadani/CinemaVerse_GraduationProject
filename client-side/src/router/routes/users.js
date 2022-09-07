export default [
  {
    path: "/register",
    name: "Register",
    meta: {
      layout: "public",
    },
    component: () =>
      import(
        /* webpackChunkName: "register" */ "../../views/Authorization/Register/Register.vue"
      ),
  },
  {
    path: "/login",
    name: "Login",
    meta: {
      layout: "public",
    },
    component: () =>
      import(
        /* webpackChunkName: "login" */ "../../views/Authorization/Login/Login.vue"
      ),
  },
  {
    path: "/admin/users",
    name: "UsersDashboard",
    meta: {
      layout: "dashboard",
    },
    component: () =>
      import(
        /* webpackChunkName: "login" */ "../../views/Admin/Users/UsersDashboard.vue"
      ),
  },
];
