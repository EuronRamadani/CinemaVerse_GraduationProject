<template>
  <div>
      <!-- :items-per-page="itemsPerPage" -->
      <!-- :page.sync="page" -->


    <v-data-table
      :headers="headers"
      :items="desserts"
      hide-default-footer
      class="elevation-1"
      @page-count="pageCount = $event"
    ></v-data-table>
   
    <div class="view">
				<router-link :to="{ name: 'Seats' }">
					Seats
				</router-link>
			</div>
   
    <!-- <div class="text-center pt-2">
      <v-pagination
        v-model="page"
        :length="pageCount"
      ></v-pagination> -->
      <!-- <v-text-field
        :value="itemsPerPage"
        label="Items per page"
        type="number"
        min="-1"
        max="15"
        @input="itemsPerPage = parseInt($event, 10)"
      ></v-text-field> -->
    <!-- </div> -->
  </div>
</template>

<script>
  export default {
    data () {
      return {
        // page: 1,
        // pageCount: 0,
        // itemsPerPage: 10,
        headers: [
          {
            text: 'DATE',
            align: 'start',
            sortable: false,
            value: 'name',
          },
          { text: 'TIME', value: 'time' },
          { text: 'TIME', value: 'time' },
          { text: 'TIME', value: 'time' },
          { text: 'TIME', value: 'time' },
          { text: 'TIME', value: 'time' },
        ],
        desserts: [
          {
            name: 'E MERKURE',
            time: '20:00',
            // fat: 6.0,
            // carbs: 24,
            // protein: 4.0,
            // iron: '1%',
          },
          {
            name: 'E ENJTE',
            time: 237,
            // fat: 9.0,
            // carbs: 37,
            // protein: 4.3,
            // iron: '1%',
          },
          {
            name: 'E PREMTE',
            time: 262,
            // fat: 16.0,
            // carbs: 23,
            // protein: 6.0,
            // iron: '7%',
          },
          {
            name: 'E SHTUNE',
            time: 305,
            // fat: 3.7,
            // carbs: 67,
            // protein: 4.3,
            // iron: '8%',
          },
          {
            name: 'E DIELE',
            time: 356,
            // fat: 16.0,
            // carbs: 49,
            // protein: 3.9,
            // iron: '16%',
          },
          {
            name: 'E MARTE',
            time: 375,
            // fat: 0.0,
            // carbs: 94,
            // protein: 0.0,
            // iron: '0%',
          },
          
        ],
      }
    },
  }
</script>