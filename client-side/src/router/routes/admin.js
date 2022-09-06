export default [
  {
    path: "/admin",
    name: "Admin",
    meta: {
      requiresAuth: true,
      layout: "dashboard",
    },
    component: () =>
      import(/* webpackChunkName: "admin" */ "../../views/Admin/Dashboard.vue"),
  },
  {
    path: "/admin/cinemas",
    name: "CinemasDashboard",
    meta: {
      requiresAuth: true,
      layout: "dashboard",
    },
    component: () =>
      import(
        /* webpackChunkName: "admin" */ "../../views/Admin/Cinemas/CinemasDashboard.vue"
      ),
  },
  {
    path: "/admin/movies",
    name: "MoviesDashboard",
    meta: {
      requiresAuth: true,
      layout: "dashboard",
    },
    component: () =>
      import(
        /* webpackChunkName: "admin" */ "../../views/Admin/Movies/MoviesDashboard.vue"
      ),
  },
  {
    path: "/admin/events",
    name: "EventsDashboard",
    meta: {
      requiresAuth: true,
      layout: "dashboard",
    },
    component: () =>
      import(
        /* webpackChunkName: "admin" */ "../../views/Admin/Events/EventsDashboard.vue"
      ),
  },
];
