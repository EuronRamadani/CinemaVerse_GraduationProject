<template>
  <div class="projects">
    <h1 class="subheading">Users</h1>
    <div class="container card mt-4">
      <table class="table m-0">
        <thead>
          <tr>
            <th scope="col">Id</th>
            <th scope="col">Display Name</th>
            <th scope="col">Email</th>
            <th scope="col">Admin</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="{ id, displayName, email, isAdmin } in users" :key="id">
            <td>{{ id }}</td>
            <td>{{ displayName }}</td>
            <td>{{ email }}</td>
            <td>{{ isAdmin ? "Admin" : "User" }}</td>
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
  name: "UsersDashboard",
  components: {},
  data() {
    return {};
  },
  methods: {
    sortBy(prop) {
      this.moviesList.sort((a, b) => (a[prop] < b[prop] ? -1 : 1));
    },
    getUsers() {
      this.$store.dispatch("getUsers").catch((error) => {
        console.log("error", error);
      });
    },
  },
  computed: {
    users() {
      return this.$store.state.users.users;
    },
  },
  created() {
    this.getUsers();
  },
};
</script>
<style scoped></style>
