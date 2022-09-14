<template>
  <div class="table-wrapper events">
    <h2>Events</h2>
    <custom-select
      v-model="selectedCinema"
      class="r-dropdown col-2 p-0 method-dropdown"
      :options="getObjectOptionsName(cinemas)"
      @change="changeCinema()"
    />
    <hr />
    <div class="d-flex mb-5 events-header">
      <div>
        <b-button class="mr-2" variant="success" @click="onCreateEvent()">
          Create Event
        </b-button>
      </div>
      <div>
        <b-button
          variant="outline-primary"
          :disabled="!isSelected"
          class="mr-2 d-lg-inline action-event-button"
          @click="onEditClick(selected[0].id)"
        >
          Edit
        </b-button>
        <button-loading
          variant="outline-primary"
          :disabled="!isSelected"
          :spinning="removingMovie"
          class="mr-2 d-lg-inline action-event-button"
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
          :items="events"
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

          <template #cell(Title)="data">
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
                no-data-text="No Events have been added to this cinema yet..."
                create-text="+ Create Event"
                access-page="Events"
                icon="Monitoring"
                @action="onCreateMovie()"
              />
            </template>
          </template>
        </b-table>
      </paper-simple>
      <table-busy v-if="loading && events.length > 0" />
    </div>
  </div>
</template>

<script>
export default {
  name: "eventDashboard",
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
        { key: "name" },
        { key: "description" },
        { key: "date" },
      ],
    };
  },
  computed: {
    loading() {
      return this.$store.state.events.loading;
    },
    events() {
      return this.$store.state.events.events;
    },
    cinemas() {
      return this.$store.state.cinemas.cinemas;
    },
    removingEvent() {
      return this.$store.state.events.removingEvent;
    },
    isSelected() {
      return this.selected[0];
    },
  },
  created() {
    this.getEvents();
  },
  methods: {
    onRowSelected(item) {
      this.selected = item;
    },
    onRefresh() {
      this.getEvents(this.selectedEvent);
    },
    getCinemas() {
      this.$store
        .dispatch("getCinemas")
        .then(() => {
          if (this.selectedEvent == null) {
            this.selectedEvent = this.events[0];
          }
          this.getEvents(this.selectedEvent);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    getEvents(selectedCinema) {
      console.log("testing " + selectedCinema.id);
      this.$store.dispatch("getEvents", selectedCinema.id).catch((error) => {
        console.log(error);
      });
    },
    changeCinema() {
      if (this.selectedCinema != null) {
        this.getEvents(this.selectedCinema);
      }
    },
    onEditClick(eventId) {
      this.$router.push({
        name: "event-edit",
        params: { eventId },
      });
    },
    onDetailsClick(eventId) {
      this.$router.push({
        name: "event-details",
        params: { eventId },
      });
    },
    onCreateEvent() {
      this.$router.push({
        name: "event-create",
      });
    },
    onDeleteClick(eventId) {
      this.confirmDelete(
        "Delete Event",
        "Are you sure you want to delete this event?"
      ).then((ok) => {
        if (ok) {
          this.$store
            .dispatch("events/removeEvent", eventId)
            .then(() => {
              this.successToast("Event was removed");
              this.selected = [];
              this.getEvents(this.selectedCinema.id);
            })
            .catch((error) => {
              console.log(error);
            });
        }
      });
    },
  },
};
</script>

<style lang="scss" scoped>
</style>
