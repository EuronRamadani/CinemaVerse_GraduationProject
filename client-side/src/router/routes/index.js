export default [
  {
    path: "/",
    name: "Home",
    component: () =>
      import(/* webpackChunkName: "homepage" */ "../../views/HomeView.vue"),
  },
  {
    path: "/aboutus",
    name: "AboutUs",
    component: () =>
      import(/* webpackChunkName: "aboutus" */ "../../views/AboutView.vue"),
  },
];
