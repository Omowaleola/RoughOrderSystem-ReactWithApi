import renderer from "react-test-renderer";
import React, {useEffect, useState} from "react";
import {OrderEditingModel, OrderProductModel, OrderSavingModel, OrderViewModel} from "../../models/order-view-model";
import {
    ClearSavedOrderProducts,
    GetOrder,
    GetOrders,
    GetSavedOrderProducts,
    InsertOrder,
    UpdateOrder
} from "../../api/orders";
import {Button, Column, Editing, Lookup} from "devextreme-react/data-grid";
import {DataGrid} from "devextreme-react";
import {ProductViewModel} from "../../models/product-view-model";
import {useAuth} from "../../contexts/auth";
import {GetOrderRequestModel} from "../../models/get-order-request-model";
import {UserType} from "../../models/user-type";
import {GetProducts, UpdateProduct} from "../../api/products";
import notify from "devextreme/ui/notify";
import {useLocation, useNavigate} from "react-router-dom";
import AddOrder from "./add-order";

jest.mock("../../models/order-view-model");
jest.mock("../../api/orders");
jest.mock("devextreme-react/data-grid");
jest.mock("devextreme-react");
jest.mock("../../models/product-view-model");
jest.mock("../../contexts/auth");
jest.mock("../../models/get-order-request-model");
jest.mock("../../models/user-type");
jest.mock("../../api/products");
jest.mock("devextreme/ui/notify");
jest.mock("react-router-dom");

const renderTree = tree => renderer.create(tree);
describe('<AddOrder>', () => {
  it('should render component', () => {
    expect(renderTree(<AddOrder 
    />).toJSON()).toMatchSnapshot();
  });
  
});