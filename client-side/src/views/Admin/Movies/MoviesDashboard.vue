<template>
  <div class="table-wrapper movies">
    <h2>Movies</h2>
    <custom-select
      v-model="selectedCinema"
      class="r-dropdown col-2 p-0 method-dropdown"
      :options="getObjectOptionsName(cinemas)"
      @change="changeCinema()"
    />
    <hr />
    <div class="d-flex mb-5 movies-header">
      <div>
        <b-button class="mr-2" variant="success" @click="onCreateMovie()">
          Create Movie
        </b-button>
      </div>
      <div>
        <b-button
          variant="outline-primary"
          :disabled="!isSelected"
          class="mr-2 d-lg-inline action-movie-button"
          @click="onEditClick(selected[0].id)"
        >
          Edit
        </b-button>
        <button-loading
          variant="outline-primary"
          :disabled="!isSelected"
          :spinning="removingMovie"
          class="mr-2 d-lg-inline action-movie-button"
          @click="onDeleteClick(selected[0].id)"
        >
          Delete
        </button-loading>
      </div>
      <button-loading
        variant="outline-primary"
        class="mt-1 mt-sm-0 ml-auto mr-0"
        :spinning="loading"
        :disabled="loading"
        @click="onRefresh()"
      >
        Refresh
      </button-loading>
    </div>
    <!-- DESKTOP -->
    <div class="default-table position-relative d-lg-block">
      <paper-simple>
        <b-table
          ref="table"
          :items="movies"
          :fields="fields"
          select-mode="single"
          show-empty
          selectable
          responsive
          :class="{ loaded: !loading }"
          :busy="loading"
          @row-selected="onRowSelected"
        >
          <template #cell(selected)="{ rowSelected }">
            <select-button :selected="rowSelected" />
          </template>

          <template #cell(title)="data">
            <a class="link" @click="onDetailsClick(data.item.id)">{{
              data.value
            }}</a>
          </template>

          <template #empty>
            <div v-if="loading" class="loading-table text-center py-1">
              <b-spinner variant="primary" />
            </div>
            <template v-else>
              <no-data
                no-data-text="No Movies have been added to this cinema yet..."
                create-text="+ Create Movie"
                access-page="Movies"
                icon="Monitoring"
                @action="onCreateMovie()"
              />
            </template>
          </template>
        </b-table>
      </paper-simple>
      <table-busy v-if="loading && movies.length > 0" />
    </div>
  </div>
</template>

<script>
export default {
  name: "Movies",
  components: {},
  data() {
    return {
      selected: [],
      selectedCinema: null,
      fields: [
        {
          key: "selected",
          label: "",
          variant: "select",
          hide: true,
        },
        { key: "id" },
        { key: "title" },
        { key: "trailerLink" },
        { key: "country" },
        { key: "language" },
        { key: "genre" },
      ],
    };
  },
  computed: {
    loading() {
      return this.$store.state.movies.loading;
    },
    movies() {
      return this.$store.state.movies.movies;
    },
    cinemas() {
      return this.$store.state.cinemas.cinemas;
    },
    removingMovie() {
      return this.$store.state.movies.removingMovie;
    },
    isSelected() {
      return this.selected[0];
    },
  },
  created() {
    this.getCinemas();
  },
  methods: {
    onRowSelected(item) {
      this.selected = item;
    },
    onRefresh() {
      this.getMovies(this.selectedCinema);
    },
    getCinemas() {
      this.$store
        .dispatch("getCinemas")
        .then(() => {
          if (this.selectedCinema == null) {
            this.selectedCinema = this.cinemas[0];
          }
          this.getMovies(this.selectedCinema);
        })
        .catch((error) => {
          this.errorToast(
            error.response?.data?.errors[0] ||
              "Something went wrong while fetching cinemas!"
          );
        });
    },
    getMovies(selectedCinema) {
      this.$store.dispatch("getMovies", selectedCinema.id).catch((error) => {
        this.errorToast(
          error.response?.data?.errors[0] ||
            "Something went wrong while fetching movies!"
        );
      });
    },
    changeCinema() {
      if (this.selectedCinema != null) {
        this.getMovies(this.selectedCinema);
      }
    },
    onEditClick(movieId) {
      this.$router.push({
        name: "movie-edit",
        params: { movieId },
      });
    },
    onDetailsClick(movieId) {
      this.$router.push({
        name: "movie-details",
        params: { movieId },
      });
    },
    onCreateMovie() {
      this.$router.push({
        name: "movie-create",
      });
    },
    onDeleteClick(movieId) {
      this.confirmDelete(
        "Delete Movie",
        "Are you sure you want to delete this movie?"
      ).then((ok) => {
        if (ok) {
          this.$store
            .dispatch("movies/removeMovie", movieId)
            .then(() => {
              this.successToast("Movie was removed");
              this.selected = [];
              this.getMovies(this.selectedCinema.id);
            })
            .catch((error) => {
              this.errorToast(error.response.data.errors[0]);
            });
        }
      });
    },
  },
};
</script>
<style lang="scss" scoped></style>
