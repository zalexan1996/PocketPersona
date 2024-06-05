import { createApp } from 'vue'
import 'bootstrap'
import './style.scss'
import App from './App.vue'
import { createPinia } from 'pinia'
import PrimeVue from 'primevue/config'

import 'primevue/resources/themes/viva-dark/theme.css'
import { createRouter, createWebHistory } from 'vue-router'
import { routes } from './routes'

let app = createApp(App)

app.use(createPinia())
app.use(PrimeVue)
app.use(createRouter({
    routes: routes,
    history: createWebHistory()
}))

app.mount('#app')