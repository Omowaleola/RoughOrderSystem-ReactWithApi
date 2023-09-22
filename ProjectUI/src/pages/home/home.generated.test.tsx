import renderer from "react-test-renderer";
import React, {useEffect, useState} from "react";

import {ProductViewModel} from "../../models/product-view-model";
import {NumberBox, Popup, TileView, ValidationSummary} from "devextreme-react";
import {GetProducts} from "../../api/products";
import notify from "devextreme/ui/notify";
import {OrderProductModel} from "../../models/order-view-model";
import {GetSavedOrderProducts, HasSavedOrderProducts, SaveOrderProduct} from "../../api/orders";
import LoadPanel from "devextreme-react/load-panel";
import Home from "./home";

jest.mock('./home.scss');
jest.mock("../../models/product-view-model");
jest.mock("devextreme-react");
jest.mock("../../api/products");
jest.mock("devextreme/ui/notify");
jest.mock("../../models/order-view-model");
jest.mock("../../api/orders");
jest.mock("devextreme-react/load-panel");

const renderTree = tree => renderer.create(tree);
describe('<Home>', () => {
  it('should render component', () => {
    expect(renderTree(<Home 
    />).toJSON()).toMatchSnapshot();
  });
  
});