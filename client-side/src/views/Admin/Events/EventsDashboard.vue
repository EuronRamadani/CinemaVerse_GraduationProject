<template>
  <div>
    <div>
      <h1>EventDashboard</h1>
      <h1>EventDashboard</h1>
      <h1>EventDashboard</h1>
    </div>
    <div class="container">
      <AddEvent />
      <EventList />
    </div>
  </div>
</template>

<script>
import AddEvent from "../../../views/Events/eventsComponents/AddEvent.vue";
import EventList from "../../../views/Events/eventsComponents/EventList.vue";

export default {
  name: "eventDashboard",
  components: { AddEvent, EventList },
};
</script>

<style></style>
