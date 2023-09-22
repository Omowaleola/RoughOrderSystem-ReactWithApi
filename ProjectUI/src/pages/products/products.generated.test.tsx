import renderer from "react-test-renderer";
import React, {useEffect, useState} from "react";
import {ProductViewModel} from "../../models/product-view-model";
import notify from "devextreme/ui/notify";
import {InsertProduct, GetProducts, DeleteProduct, UpdateProduct} from "../../api/products";
import {NumberBox, Popup, ValidationSummary} from "devextreme-react";
import DataGrid, {
    Button,
    Column,
    Editing,
    HeaderFilter,
    Paging,
    SearchPanel,
} from 'devextreme-react/data-grid'
import {useAuth} from "../../contexts/auth";
import {UserType} from "../../models/user-type";
import {RequiredRule} from "devextreme-react/form";
import LoadPanel from "devextreme-react/load-panel";
import {GetSavedOrderProducts, HasSavedOrderProducts, SaveOrderProduct} from "../../api/orders";
import {OrderProductModel} from "../../models/order-view-model";
import Products from "./products";

jest.mock("../../models/product-view-model");
jest.mock("devextreme/ui/notify");
jest.mock("../../api/products");
jest.mock("devextreme-react");
jest.mock('devextreme-react/data-grid');
jest.mock("../../contexts/auth");
jest.mock("../../models/user-type");
jest.mock("devextreme-react/form");
jest.mock("devextreme-react/load-panel");
jest.mock("../../api/orders");
jest.mock("../../models/order-view-model");

const renderTree = tree => renderer.create(tree);
describe('<Products>', () => {
  it('should render component', () => {
    expect(renderTree(<Products 
    />).toJSON()).toMatchSnapshot();
  });
  
});