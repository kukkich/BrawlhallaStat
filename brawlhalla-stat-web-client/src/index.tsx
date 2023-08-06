import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import {Provider} from "react-redux";
import rootStore from "./store/rootStore"; // Импортируйте вашу тему из файла Theme.ts

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

function Root() {

    return (
        <Provider store={rootStore}>
            <App />
        </Provider>
    );
}

root.render(
    <Root />
);
