<template>
	<div>
		<input type="text" v-model="search" placeholder="Search cinemas" />
		<div class="card" v-for="cinema in filter" :key="cinema.id">
			<v-card class="mx-auto my-12" max-width="374">
				<template slot="progress">
					<v-progress-linear
						color="deep-purple"
						height="10"
						indeterminate
					></v-progress-linear>
				</template>

				<v-img
					height="250"
					src="https://cdn.vuetifyjs.com/images/cards/cooking.png"
				></v-img>

				<v-card-title>{{ cinema.title }}</v-card-title>

				<p class="ml-5">
					{{ cinema.vendi }}
				</p>

				<v-divider class="mx-4"></v-divider>

				<!-- <v-card-title>Tonight's availability</v-card-title> -->

				<v-card-actions>
					<v-btn color="deep-purple lighten-2" text :to="{ name: 'Cineplexx' }">
						View Movies
					</v-btn>
					<v-btn
						color="deep-purple lighten-2"
						text
						:to="{ name: 'CineplexxPrice' }"
					>
						See Prices
					</v-btn>
					<v-btn
						color="deep-purple lighten-2"
						text
						:to="{ name: 'CineplexxInfo' }"
					>
						Info
					</v-btn>
				</v-card-actions>
			</v-card>
		</div>
	</div>
</template>

<!--<template>
	<div>
		<v-card outlined shaped>
			<v-divider class="">
				<img
					src="@/assets/joker.jpg"
					style="height: 100px; width: 100px"
					alt=""
				/>
			</v-divider>
			<h1>Cineplexx</h1>
			<p>Subtitle text</p>
		</v-card>
	</div>
</template>-->

<!--<template>
	<div>
		<v-toolbar color="purple" height="200px">
			<v-toolbar-title>Title</v-toolbar-title>
			<img
				src="@/assets/joker.jpg"
				style="height: 100px; width: 150px"
				alt=""
			/>

			<v-divider class="mx-4" vertical></v-divider>

			<v-card color="">
				<h1>Cineplexx</h1>
				<p>Prishtine / Prizren</p>
				<p><b> 2D, 3D, 4K</b></p>
			</v-card>

			<v-spacer></v-spacer>

			<v-card color="">
				<router-link :to="{ name: 'Price' }">
					<button>Price</button> <br />
				</router-link>
				<v-spacer></v-spacer>
				<router-link :to="{ name: 'Movies' }">
					<button>View Movies</button>
				</router-link>
			</v-card>
		</v-toolbar>
		<v-toolbar color="purple" height="200px">
			 <v-toolbar-title>Title</v-toolbar-title>
			<img
				src="@/assets/joker.jpg"
				style="height: 100px; width: 150px"
				alt=""
			/>

			<v-divider class="mx-4" vertical></v-divider>

			<v-card color="">
				<h1>Kinemaja ABC</h1>
				<p>Prishtine</p>
				<p><b> 2D, 3D, 4K</b></p>
			</v-card>

			<v-spacer></v-spacer>

			<v-card color="">
				<router-link :to="{ name: 'Price' }">
					<button>Price</button> <br />
				</router-link>
				<v-spacer></v-spacer>
				<router-link :to="{ name: 'Movies' }">
					<button>View Movies</button>
				</router-link>
			</v-card>
		</v-toolbar>
	</div>
</template>-->

<script>
export default {
	name: "Cinemas",
	data() {
		return {
			cinemas: [
				{
					title: "Cineplexx Cinema",
					vendi: "Prishtine / Prizren",
					cmimi: { name: "CineplexxPrice" },
				},
				{ title: "ABC Cinema", vendi: "Prishtine" },
			],
			// cinemas: [],
			search: "",
		};
	},
	computed: {
		filter() {
			return this.cinemas.filter((temp) => {
				return temp.title.match(this.search);
			});
		},
	},
};
</script>

<style scoped>
.card {
	flex-direction: row;
	justify-content: center;
}
input {
	border: 1px solid black;
	padding: 20px;
	margin-top: 2%;
	margin-bottom: 2%;
	border-radius: 15px;
}
</style>
