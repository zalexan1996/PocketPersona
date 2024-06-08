import { createApp } from 'vue'
import Bootstrap from 'bootstrap'
import './style.scss'
import App from './App.vue'
import { createPinia } from 'pinia'
import PrimeVue from 'primevue/config'

import { createRouter, createWebHistory } from 'vue-router'
import { routes } from './routes'
import { library} from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faTrash, faEdit, faPlus } from '@fortawesome/free-solid-svg-icons'

library.add(faTrash)
library.add(faEdit)
library.add(faPlus)

let app = createApp(App)

app.use(createPinia())
app.use(PrimeVue)
app.use(createRouter({
    routes: routes,
    history: createWebHistory()
}))

app.component('font-awesome-icon', FontAwesomeIcon)

app.mount('#app')