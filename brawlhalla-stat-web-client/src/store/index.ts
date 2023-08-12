import {AppDispatch, AppStore, RootState, rootStore } from "./root";
import {useRootDispatch, useRootSelector} from "./hooks/useRootDispatch";


export type {RootState, AppStore, AppDispatch }
export {useRootDispatch, useRootSelector, rootStore}