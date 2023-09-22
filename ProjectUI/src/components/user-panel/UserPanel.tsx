import React, { useMemo } from 'react';
import { useNavigate } from "react-router-dom";
import ContextMenu, { Position } from 'devextreme-react/context-menu';
import List from 'devextreme-react/list';
import { useAuth } from '../../contexts/auth';
import './UserPanel.scss';
import type { UserPanelProps } from '../../types';
import {deleteCustomer} from "../../api/auth";
import notify from "devextreme/ui/notify";

export default function UserPanel({ menuMode }: UserPanelProps) {
  const { user, signOut } = useAuth();
  const navigate = useNavigate();

  async function DeleteAccount() {
    const userId = user?.id;
    const result = await deleteCustomer(userId);
    if(result.isOk)
    {
      signOut();
      notify('Account Deleted Successfully', 'success',3000);
    }else{
      notify(result.message, 'error',3000);
    }
    

  }
  
  const menuItems = useMemo(() => ([
    {
      text: 'Delete Account',
      icon: 'trash',
      onClick: DeleteAccount
    },
      {
      text: 'Logout',
      icon: 'runner',
      onClick: signOut
    }
  ]), [signOut]);
  return (
    <div className={'user-panel'}>
      <div className={'user-info'}>
        <div className={'image-container'}>
          <div
            style={{
              backgroundColor: 'red',
              backgroundSize: 'cover'
            }}
            className={'user-image'} />
        </div>
        <div className={'user-name'}>{user!.name}</div>
      </div>

      {menuMode === 'context' && (
        <ContextMenu
          items={menuItems}
          target={'.user-button'}
          showEvent={'dxclick'}
          width={210}
          cssClass={'user-menu'}
        >
          <Position my={{ x: 'center', y: 'top' }} at={{ x: 'center', y: 'bottom' }} />
        </ContextMenu>
      )}
      {menuMode === 'list' && (
        <List className={'dx-toolbar-menu-action'} items={menuItems} />
      )}
    </div>
  );
}
