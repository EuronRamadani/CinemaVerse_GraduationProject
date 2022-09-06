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
