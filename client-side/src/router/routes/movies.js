export default [
  {
    path: "/movies",
    name: "Movies",
    meta: {
      layout: "public",
    },
    component: () =>
      import(
        /* webpackChunkName: "register" */ "../../views/Movies/Movies.vue"
      ),
  },
  {
    path: "/movie/:id",
    name: "Movie",
    component: () =>
      import(
        /* webpackChunkName: "register" */ "../../views/Movies/MovieDetails.vue"
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
        /* webpackChunkName: "movies-dashboard" */ "../../views/Admin/Movies/MoviesDashboard.vue"
      ),
  },
  {
    path: "/admin/movies/create",
    name: "movie-create",
    meta: {
      requiresAuth: true,
      layout: "dashboard",
    },
    component: () =>
      import(
        /* webpackChunkName: "create-movie" */ "../../views/Admin/Movies/CreateMovie.vue"
      ),
  },
  {
    path: "/admin/movies/details/:movieId",
    name: "movie-details",
    meta: {
      requiresAuth: true,
      layout: "dashboard",
    },
    component: () =>
      import(
        /* webpackChunkName: "movie-details" */ "../../views/Admin/Movies/MovieDetails.vue"
      ),
  },
  {
    path: "/admin/movies/edit/:movieId",
    name: "movie-edit",
    meta: {
      requiresAuth: true,
      layout: "dashboard",
    },
    component: () =>
      import(
        /* webpackChunkName: "movie-details" */ "../../views/Admin/Movies/EditMovie.vue"
      ),
  },

  //static pages
  {
    path: "/cineplexx/movies",
    name: "Cineplexx",
    meta: {
      layout: "public",
    },
    component: () =>
      import(
        /* webpackChunkName: "register" */ "../../views/Movies/Cineplexx/CineplexxMovies.vue"
      ),
  },
  {
    path: "/abc/movies",
    name: "ABC",
    meta: {
      layout: "public",
    },
    component: () =>
      import(
        /* webpackChunkName: "register" */ "../../views/Movies/ABC/ABCMovies.vue"
      ),
  },
];
