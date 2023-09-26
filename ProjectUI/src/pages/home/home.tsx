import React, {useEffect, useState} from 'react';
import './home.scss';
import {ProductViewModel} from "../../models/product-view-model";
import {NumberBox, Popup, TileView, ValidationSummary} from "devextreme-react";
import {GetProducts} from "../../api/products";
import notify from "devextreme/ui/notify";
import {OrderProductModel} from "../../models/order-view-model";
import {GetSavedOrderProducts, HasSavedOrderProducts, SaveOrderProduct} from "../../api/orders";
import LoadPanel from "devextreme-react/load-panel";

export default function Home() {
    const [products, setProducts] = useState<ProductViewModel[]>();
    const [loading, setLoading] = useState(true);
    const [isPopupVisible, setPopupVisibility] = useState(false);
    const [selectedProduct, setSelectedProduct] = useState<ProductViewModel>(null);
    const [selectedQuantity, setSelectedQuantity] = useState<number>(1);
    const ClickedProduct = (e) => {
        setSelectedProduct(e.itemData);
        setPopupVisibility(true);
    }
    const productTile = (data) => {
        return (
            <div title={data.description}>
                <h5 className="centerText">{data.name}</h5>
                <h6 className="centerText">R {data.price}</h6>
            </div>);
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
        notify('Product added to Wish List', 'success', 3000);
        
    };
    const popupContent = () => {
        return (<form onSubmit={addProductToWishList}>
            <h6>{selectedProduct?.name}</h6>
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
            <LoadPanel
                visible={loading}
            />
            <TileView items={products}
                      itemRender={productTile}
                      baseItemHeight={250}
                      baseItemWidth={250}
                      direction="vertical"
                      onItemClick={ClickedProduct}

            />
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
