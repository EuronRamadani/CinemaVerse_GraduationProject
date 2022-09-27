export default [
  //Admin Routes
  {
    path: "/admin/movie-times",
    name: "MovieTimesDashboard",
    meta: {
      requiresAuth: true,
      layout: "dashboard",
    },
    component: () =>
      import(
        /* webpackChunkName: "movie-times-dashboard" */ "../../views/Admin/MovieTimes/MovieTimesDashboard.vue"
      ),
  },
  {
    path: "/admin/movie-times/cinemaId/:cinemaId/:movieId",
    name: "movie-times",
    meta: {
      requiresAuth: true,
      layout: "dashboard",
    },
    component: () =>
      import(
        /* webpackChunkName: "movie-times" */ "../../views/Admin/MovieTimes/MovieTimesDashboard.vue"
      ),
  },
  {
    path: "/admin/movie-times/cinemaId/:cinemaId/:movieId/create",
    name: "movie-times-create",
    meta: {
      requiresAuth: true,
      layout: "dashboard",
    },
    component: () =>
      import(
        /* webpackChunkName: "create-hall" */ "../../views/Admin/MovieTimes/CreateMovieTimes.vue"
      ),
  },
];
