<template>
  <div class="card mt-4">
    <table class="table m-0">
      <thead>
        <tr>
          <th scope="col">Image</th>
          <th scope="col">Name</th>
          <th scope="col">Description</th>
          <th scope="col">Is paid?</th>
          <th scope="col">Action</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="{ id, name, image, description, isPaid } in events"
          :key="id"
        >
          <td>{{ image }}</td>
          <td>{{ name }}</td>
          <td>{{ description }}</td>
          <td>{{ isPaid }}</td>
          <td>
            <router-link :to="`/edit/${id}`">
              <button class="btn btn-primary btn-sm me-2">Edit</button>
            </router-link>
            <button class="btn btn-danger btn-sm" @click="deleteEvent(id)">
              Delete
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  name: "eventList",
};
</script>
