<template>
  <div class="chart-wrapper">
    <Line :data="chartData" :style="myStyles" />
  </div>
</template>

<script lang="ts">
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
} from 'chart.js'
import { Line } from 'vue-chartjs'

ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
)

export default {
  name: 'LineChart',
  components: {Line},
  computed: {
    myStyles() {
      return {
        height: '400px',
        width: '750px',
      }
    }
  },
  props: {
    chartData: {
      type: Object,
      required: true
    },
    chartOptions: {
      type: Object,
      default: () => {
      }
    }
  },
}
</script>

<style scoped>

.chart-wrapper {
  display: flex;
  align-items: center;
  justify-content: center;
}

</style>
