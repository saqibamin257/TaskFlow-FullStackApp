"use client";

import { useState } from "react";

import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";

import { Button } from "@/components/ui/button";

import { CreateOrganizationForm } from "./create-organization-form";

export function CreateOrganizationDialog() {
  const [open, setOpen] = useState(false);

  const handleSuccess = () => {
    setOpen(false);
  };

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger render={<Button />}>Create Organization</DialogTrigger>

      <DialogContent className="sm:max-w-lg">
        <DialogHeader>
          <DialogTitle>Create Organization</DialogTitle>

          <DialogDescription>
            Create a new organization and configure its basic information.
          </DialogDescription>
        </DialogHeader>

        <CreateOrganizationForm onSuccess={handleSuccess} />
      </DialogContent>
    </Dialog>
  );
}
