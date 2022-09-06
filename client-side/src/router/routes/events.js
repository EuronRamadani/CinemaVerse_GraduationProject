export default [
  {
    path: "/events",
    name: "Events",
    meta: {
      layout: "public",
    },
    component: () =>
      import(
        /* webpackChunkName: "register" */ "../../views/Events/Events.vue"
      ),
  },
];
