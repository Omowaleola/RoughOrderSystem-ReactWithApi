import {HomePage, OrdersPage, ProductsPage} from './pages';
import { withNavigationWatcher } from './contexts/navigation';

const routes = [
    {
        path: '/orders',
        element: OrdersPage
    },
    {
        path: '/products',
        element: ProductsPage
    },
    {
        path: '/home',
        element: HomePage
    }
];

export default routes.map(route => {
    return {
        ...route,
        element: withNavigationWatcher(route.element, route.path)
    };
});
