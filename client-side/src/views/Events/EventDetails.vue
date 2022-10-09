<template>
	<div>
		<section id="stats">
			<v-parallax
				:height="$vuetify.breakpoint.smAndDown ? 700 : 500"
				:src="img"
			>
				<v-container fill-height>
					<v-row>
						<!-- v-for="[title] of stats" :key="title" cols="12" md="4" -->
						<v-col>
							<div class="text-center">
								<div class="display-3 font-weight-black mb-4 md=1">
									{{ event.name }}
								</div>

								<div class="title font-weight-regular text-uppercase">
									{{ formatSimpleDateTime(event.date) }}
								</div>
								<div class="title font-weight-regular text-uppercase">
									{{ event.price }}€
								</div>
							</div>
						</v-col>
					</v-row>
				</v-container>
			</v-parallax>
		</section>
		<section id="welcome" class="overflow-hidden">
			<v-row no-gutters>
				<v-col class="hidden-sm-and-down test" md="6">
					<h1 class="text-center">What is the Event?</h1>
				</v-col>
				<v-col
					class="align-content-space-between layout wrap"
					cols="12"
					md="6"
					:pa-5="$vuetify.breakpoint.smAndDown"
				>
					<v-row align="center" justify="center" style="margin-top:25px; margin-bottom:25px;">
						<v-col cols="10" md="8">
							<p>
								{{ event.description }}
							</p>
						</v-col>
					</v-row>
				</v-col>
			</v-row>
		</section>
		<v-divider></v-divider>
		<div>
			<h1 class="text-center">Images</h1>
			<EventCardDetail :event="event" />
		</div>

		<!-- <div class="container">
			<div class="header">
				<img :src="event.photos[0].imgClientPath" alt="no" />
				<div class="informationDiv">
					<h3>{{ event.name }}</h3>
					<p>
						This event is being hosted aaaat {{ cinema.name }} in the city of
						{{ cinema.city }} with the address {{ cinema.address }}
					</p>
					<p v-if="event.isPaid">
						This event will cost {{ event.price }}€ to attend
					</p>
				</div>
			</div>
			<div class="content">
				<h4>{{ event.name }}</h4>
				<p>
					{{ event.description }}
				</p>
			</div>
			<div>
				<button-f buttonText="Go to event" />
			</div>
		</div> -->
	</div>
</template>

<script>
// import ButtonF from "../../components/uiComponents/ButtonF.vue";
import { required, numberInt, minValueRule } from "@/helpers/validations";
import { setInteractionMode } from "vee-validate";
import EventCardDetail from "@/components/EventCardPhoto.vue";

setInteractionMode("eager");
export default {
	name: "Event",
	components: { EventCardDetail },

	data() {
		return {
			cinemaId: null,
			required,
			numberInt,
			minValueRule,
			img: "https://img.freepik.com/premium-photo/background-empty-red-dark-podium-with-lights-tile-floor-3d-rendering_314485-400.jpg?w=2000",
			stats: [
				["50+", "Atteendees"],
				["89", "Price"],
			],
		};
	},
	created() {
		this.eventId = this.$route.params.eventId;
		this.cinemaId = this.cinema.id;
		this.getEvent(this.eventId);
	},
	filters: {
		truncate: function (text, length, suffix) {
			if (text.length > length) {
				return text.substring(0, length) + suffix;
			} else {
				return text;
			}
		},
	},
	computed: {
		loading() {
			return this.$store.state.events.loading;
		},
		event() {
			return this.$store.state.events.event;
		},
		cinema() {
			return this.$store.state.cinemas.cinema;
		},
		user() {
			return this.$store.state.users.user;
		},
	},
	methods: {
		submit() {
			this.$refs.observer.validate();
		},
		getEvent(eventId) {
			const query = {
				cinemaId: this.cinemaId,
				eventId: eventId,
			};
			this.$store
				.dispatch("getEvent", query)
				.then(() => {})
				.catch((error) => {
					this.errorToast(
						error.response?.data?.errors[0] ||
							"Something went wrong while fetching event details!"
					);
				});
		},
	},
};
</script>

<style lang="scss" scoped>
.test {
	background-color: rgb(196, 179, 179);

	h1 {
		margin-top: revert;
	}
}
</style>
