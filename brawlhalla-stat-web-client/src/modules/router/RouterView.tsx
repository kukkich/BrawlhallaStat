import React from 'react';
import {Route, Routes} from "react-router-dom";
import Layout from "../../App/Components/Layout";
import SandBoxPage from "../../App/Components/SandBoxPage";
import {AnonymousOnly} from "./politics/AnonymousOnly";
import {AuthPage} from "../authentication";
import {AuthRequired} from "./politics/AuthRequired";
import { LegendsPage } from '../statistic/legend';

const RouterView = () => {
    return (
        <Routes>
            <Route element={<Layout />}>
                <Route path="/" element={<SandBoxPage />} />
                <Route path="/auth" element={
                    <AnonymousOnly>
                        <AuthPage />
                    </AnonymousOnly>
                } />
                <Route path="/protected" element={
                    <AuthRequired>
                        <div>Protected</div>
                    </AuthRequired>
                } />
                <Route path="/legends" element={
                        <LegendsPage/>
                } />
            </Route>
        </Routes>
    );
};

export default RouterView;