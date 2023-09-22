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

export default function Products() {
    const [products, setProducts] = useState<ProductViewModel[]>();
    const {user} = useAuth();
    const [loading, setLoading] = useState(true);
    const [isPopupVisible, setPopupVisibility] = useState(false);
    const [selectedProduct, setSelectedProduct] = useState<ProductViewModel>(null);
    const [selectedQuantity, setSelectedQuantity] = useState<number>(1);
    const emptyProduct: ProductViewModel = {id: '', name: '', description: '', price: 0};

    const AddProduct = async (e) => {
        setLoading(true);
        const prod = Object.assign({}, emptyProduct, e.data);
        const result = await InsertProduct(prod);
        if (result.isOk) {
            const getResult = await GetProducts();
            if (getResult.isOk) {
                setProducts(getResult.data);
            } else {
                notify(getResult.data, 'error', 2000);
            }
            setLoading(false);

        } else {
            notify(result.data, 'error', 2000);
        }
        setLoading(false);
    };
    const RemovedProduct = async (e) => {
        const result = await DeleteProduct([e.key]);
        if (result.isOk) {
            const getResult = await GetProducts();
            if (getResult.isOk) {
                setProducts(getResult.data);
            } else {
                notify(getResult.data, 'error', 2000);
            }
            setLoading(false);

        } else {
            notify(result.data, 'error', 2000);
        }
        setLoading(false);

    };
    const EditProduct = async (e) => {
        setLoading(true);
        const prod = Object.assign({}, e.oldData, e.newData);
        const result = await UpdateProduct(prod);
        if (result.isOk) {
            const getResult = await GetProducts();
            if (getResult.isOk) {
                setProducts(getResult.data);
            } else {
                notify(getResult.data, 'error', 2000);
            }
            setLoading(false);

        } else {
            notify(result.data, 'error', 2000);
        }
        setLoading(false);
    };

    const selectProduct = (e) => {
        setSelectedProduct(e.row.data);
        setPopupVisibility(true);
    };

    const addProductToWishList = (e) => {
        setPopupVisibility(false);
        const orderProd: OrderProductModel = {
            id: 0,
            productId: selectedProduct?.id,
            quantity: selectedQuantity
        };
        setSelectedProduct(null);
        setSelectedQuantity(1);
        if (HasSavedOrderProducts()) {
            const savedProducts = GetSavedOrderProducts();
            const currentProdInStorage = savedProducts.find(p=>p.productId === orderProd.productId);
            const index = savedProducts.indexOf(currentProdInStorage, 0);
            if (index > -1) {
                savedProducts.splice(index, 1);
                orderProd.quantity += currentProdInStorage.quantity;
            }
            savedProducts.push(orderProd);         
            SaveOrderProduct(savedProducts);
        } else {
            SaveOrderProduct([orderProd]);
        }
    };

    const popupContent = () => {
        return (<form onSubmit={addProductToWishList}>
            <h5>{selectedProduct?.name}</h5>
            <div className="dx-field">
                <div className="dx-field-label">Quantity To Add:</div>
                <div className="dx-field-value">
                    <NumberBox
                        value={selectedQuantity}
                        onValueChange={e => setSelectedQuantity(e)}
                        min={1}
                        showSpinButtons={true}
                        width={100}>
                    </NumberBox>
                </div>
            </div>
            <div className="dx-fieldset">
                <ValidationSummary id="summary"/>
                <button type="submit"> Add</button>
            </div>
        </form>);
    }
    useEffect(() => {
        (async function () {
            const result = await GetProducts();
            if (result.isOk) {
                setProducts(result.data);
            } else {
                notify(result.data, 'error', 2000);
            }
            setLoading(false);
        })();
    }, []);
    return (
        <React.Fragment>
            <h2>Products</h2>
            <LoadPanel
                visible={loading}
            />
            <DataGrid
                dataSource={products}
                onRowInserting={AddProduct}
                onRowRemoving={RemovedProduct}
                onRowUpdating={EditProduct}
                keyExpr="id">
                <Paging defaultPageSize={10} />
                <HeaderFilter visible={true} />
                <SearchPanel visible={true} />
                <Editing
                    mode="form"
                    allowAdding={user?.userRole === UserType.admin}
                    allowUpdating={user?.userRole === UserType.admin}
                    allowDeleting={user?.userRole === UserType.admin}
                />
                <Column dataField="name">
                    <RequiredRule message="Product Name is required"/>
                </Column>
                <Column dataField="description">
                    <RequiredRule message="Description is required"/>
                </Column>
                <Column dataField="price" dataType="number">
                    <RequiredRule message="Price is required"/>
                </Column>
                <Column type="buttons">
                    <Button name="edit" icon="edit"/>
                    <Button name="delete" icon="trash"/>
                    <Button icon="like" onClick={selectProduct} visible={user?.userRole !== UserType.admin}/>
                </Column>
            </DataGrid>

            <Popup
                visible={isPopupVisible}
                hideOnOutsideClick={false}
                width={400}
                height={400}
                showCloseButton={true}
                showTitle={true}
                title="Add To Wishlist"
                contentRender={popupContent}>

            </Popup>
        </React.Fragment>

    )
}