export default [
  {
    path: "/cinemas",
    name: "Cinemas",
    meta: {
      layout: "public",
    },
    component: () =>
      import(
        /* webpackChunkName: "register" */ "../../views/Cinemas/Cinemas.vue"
      ),
  },
];
