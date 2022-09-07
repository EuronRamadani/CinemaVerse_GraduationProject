<template>
  <div class="card card-body mt-4">
    <form @submit.prevent="onSubmit">
      <div class="form-group">
        <label>Event Name</label>
        <input v-model="form.name" class="form-control" required />
      </div>

      <div class="form-group mt-3">
        <label>Description</label>
        <input
          v-model="form.description"
          class="form-control"
          type="text"
          required
        />
      </div>

      <div class="form-group mt-3">
        <label>Is Paid?</label>
        <input
          v-model="form.isPaid"
          class="form-control"
          type="text"
          required
        />
      </div>

      <button type="submit" class="btn btn-success mt-3">Create Event</button>
    </form>
  </div>
</template>

<script>
export default {};
</script>

<style></style>
