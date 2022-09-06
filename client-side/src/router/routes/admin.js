export default [
  {
    path: "/admin",
    name: "Admin",
    meta: {
      requiresAuth: true,
    },
    component: () =>
      import(/* webpackChunkName: "admin" */ "../../views/Admin/Dashboard.vue"),
  },
];
