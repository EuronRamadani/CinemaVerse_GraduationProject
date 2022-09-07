<template>
  <div class="container">
    <AddEvent />
    <EventList />
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

<style>
</style>