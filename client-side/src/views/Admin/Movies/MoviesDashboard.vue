<template>
  <div class="projects">
    <h1 class="subheading" @click="getMovies">Movies</h1>
    <div class="container card mt-4">
      <table class="table m-0">
        <thead>
          <tr>
            <th scope="col">Title</th>
            <th scope="col">Trailer Link</th>
            <th scope="col">Release Year</th>
            <th scope="col">Country</th>
            <th scope="col">Language</th>
            <th scope="col">Genre</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="{
              title,
              trailerLink,
              releaseYear,
              country,
              language,
              genre,
              id,
            } in movies"
            :key="id"
          >
            <td>{{ title }}</td>
            <td>{{ trailerLink }}</td>
            <td>{{ releaseYear }}</td>
            <td>{{ country }}</td>
            <td>{{ language }}</td>
            <td>{{ genre }}</td>
            <!-- <td>
              <router-link :to="`/edit/${id}`">
                <button class="btn btn-primary btn-sm me-2">Edit</button>
              </router-link>
              <button class="btn btn-danger btn-sm" @click="deleteEvent(id)">
                Delete
              </button>
            </td> -->
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  name: "MoviesDashboard",
  components: {},
  data() {
    return {};
  },
  methods: {
    sortBy(prop) {
      this.moviesList.sort((a, b) => (a[prop] < b[prop] ? -1 : 1));
    },
    getMovies() {
      this.$store.dispatch("getMovies").catch((error) => {
        console.log("error", error);
      });
    },
  },
  computed: {
    movies() {
      return this.$store.state.movies.movies;
    },
  },
  created() {
    this.getMovies();
    console.log(this.movies);
  },
};
</script>
<style scoped></style>
