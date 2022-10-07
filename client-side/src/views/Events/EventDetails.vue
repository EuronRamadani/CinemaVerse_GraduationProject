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
					<v-row align="center" justify="center">
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
				// ["145+", "Movies Streamed"],
				// ["2300+", "Visits"],
				["89", "Price"],
			],
		};
	},
	created() {
		this.eventId = this.$route.params.eventId;
		this.cinemaId = this.cinema.id;
		this.getEvent(this.eventId);
		// console.log("getEVENT", this.getEvent(this.eventId));
		console.log("testiiiii", this.event);
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
		// photo(){
		// 	return this.$store.sate
		// },
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
		align-items: center;
		margin-top: 20%;
	}
}
.container {
	margin-right: 10%;
	margin-left: 10%;
	width: 80%;
	padding: 0;
	height: auto;
	border-radius: 20px;
	border: 3px solid black;

	.header {
		border-radius: 20px !important;
		width: 100%;
		height: auto;
		display: flex;
		flex-direction: row;
		flex-wrap: nowrap;

		img {
			width: 50%;
			height: 400px;
			border-radius: 20px !important;
		}

		.informationDiv {
			width: 50%;
			height: 400px;
			padding: 16px;
			display: flex;
			flex-direction: column;

			div {
				width: 100%;
				height: 50%;
			}

			p {
				font-size: 22px;
				color: rgb(25, 118, 210);
			}
		}
	}

	@media (max-width: 900px) {
		.header {
			flex-direction: column;
			width: 100%;

			img {
				width: 100%;
			}

			.informationDiv {
				width: 100%;

				h3 {
					text-align: center;
				}

				p {
					text-align: center;
				}
			}
		}
	}

	.content {
		height: auto;
		width: 100%;
		padding: 15px;

		h4 {
			text-align: center;
			font-size: 32px;
			margin-bottom: 3%;
			margin-top: 3%;
			color: black;
		}

		p {
			font-size: 22px;
			padding: 10px;
			color: rgb(25, 118, 210);
		}
	}
}
</style>
