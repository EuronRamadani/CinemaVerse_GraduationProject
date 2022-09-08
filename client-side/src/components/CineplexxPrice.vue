<template>
	<v-simple-table>
		<template v-slot:default>
			<thead>
				<tr>
					<th class="text-left">Price for</th>
					<th class="text-left">Euro</th>
				</tr>
			</thead>
			<tbody>
				<tr v-for="item in desserts" :key="item.name">
					<td>{{ item.name }}</td>
					<td>{{ item.calories }}</td>
				</tr>
			</tbody>
		</template>
	</v-simple-table>
</template>

<script>
export default {
	data() {
		return {
			desserts: [
				{
					name: "FËMIJËT TË ANËTARËSUAR NË KLUBIN FAMILJAR TË FILMIT",
					calories: "2,80 €",
				},
				{
					name: "E HËNË, E MËRKURË DHE E ENJTE.",
					calories: "3,80 €",
				},
				{
					name: "STUDENTËT (E HËNË, E MËRKURË, E ENJTE)",
					calories: "3,20 €",
				},
				{
					name: "GRUPET",
					calories: "3,00 €",
				},
				{
					name: "DITA E KINEMASË (E MARTË)",
					calories: "3,00 €",
				},
				{
					name: "PERSONA ME AFTËSI TË KUFIZUARA",
					calories: "2,50 €",
				},
				{
					name: "E PREMTE DERI TË DIELË + FESTA ZYRTARE",
					calories: "4,10 €",
				},
				{
					name: "ULËSET VIP",
					calories: "+ 1,00 €",
				},
				{
					name: "PËR FILMAT MBI 120 MINUTA",
					calories: "+ 0,30 €",
				},
				{
					name: "PËR FILMAT MBI 140 MINUTA",
					calories: "+ 0,80 €",
				},
			],
		};
	},
};
</script>
