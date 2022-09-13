<template>
  <div class="table-wrapper cinemas">
    <h2>Cinemas</h2>
    <hr />
    <div class="d-flex mb-5 cinemas-header">
      <div>
        <b-button class="mr-2" variant="success" @click="onCreateCinema()">
          Create Cinema
        </b-button>
      </div>
      <div>
        <b-button
          variant="outline-primary"
          :disabled="!isSelected"
          class="mr-2 d-lg-inline action-cinema-button"
          @click="onEditClick(selected[0].id)"
        >
          Edit
        </b-button>
        <button-loading
          variant="outline-primary"
          :disabled="!isSelected"
          :spinning="removingCinema"
          class="mr-2 d-lg-inline action-cinema-button"
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
          :items="cinemas"
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

          <template #cell(name)="data">
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
                no-data-text="No Cinemas have been added yet..."
                create-text="+ Create Cinema"
                access-page="Cinemas"
                icon="Monitoring"
                @action="onCreateCinema()"
              />
            </template>
          </template>
        </b-table>
      </paper-simple>
      <table-busy v-if="loading && cinemas.length > 0" />
    </div>
  </div>
</template>

<script>
export default {
  name: "Cinemas",
  components: {},
  data() {
    return {
      selected: [],
      fields: [
        {
          key: "selected",
          label: "",
          variant: "select",
          hide: true,
        },
        { key: "id" },
        { key: "name" },
        { key: "city" },
        { key: "address" },
        { key: "numberOfVenues" },
      ],
    };
  },
  computed: {
    loading() {
      return this.$store.state.cinemas.loading;
    },
    cinemas() {
      return this.$store.state.cinemas.cinemas;
    },
    removingCinema() {
      return this.$store.state.cinemas.removingCinema;
    },
    isSelected() {
      return this.selected[0];
    },
  },
  watch: {
    currentPage() {
      this.getCinemas();
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
      this.getCinemas();
    },
    getCinemas() {
      this.$store.dispatch("getCinemas").catch((error) => {
        this.errorToast(
          error.response?.data?.errors[0] ||
            "Something went wrong while fetching cinemas!"
        );
      });
    },
    onEditClick(cinemaId) {
      this.$router.push({
        name: "cinema-edit",
        params: { cinemaId },
      });
    },
    onDetailsClick(cinemaId) {
      this.$router.push({
        name: "cinema-details",
        params: { cinemaId },
      });
    },
    onCreateCinema() {
      this.$router.push({
        name: "cinema-create",
      });
    },
    onDeleteClick(cinemaId) {
      this.confirmDelete(
        "Delete Cinema",
        "Are you sure you want to delete this cinema?"
      ).then((ok) => {
        if (ok) {
          this.$store
            .dispatch("cinemas/removeCinema", cinemaId)
            .then(() => {
              this.successToast("Cinema was removed");
              this.selected = [];
              this.getCinemas();
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
