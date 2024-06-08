import { RouteRecordRaw } from "vue-router";
import Home from './views/Home.vue';
import Arcana from "./views/Arcana.vue";
import Characters from "./views/Characters.vue";
import SocialLinks from "./views/SocialLinks.vue";
import Games from "./views/Games.vue";

export const routes: RouteRecordRaw[] = [
    {
        name: 'home',
        path: '/',
        component: Home
    },
    {
        name: 'arcana',
        path: '/arcana',
        component: Arcana
    },
    {
        name: 'characters',
        path: '/characters',
        component: Characters
    },
    {
        name: 'social-links',
        path: '/social-links',
        component: SocialLinks
    },
    {
        name: 'games',
        path: '/games',
        component: Games
    }
]