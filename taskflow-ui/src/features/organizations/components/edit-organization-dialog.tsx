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

import { Edit } from "lucide-react";

import { EditOrganizationForm } from "./edit-organization-form";

import type { Organization } from "../types/organization.types";

interface EditOrganizationDialogProps {
  organization: Organization;
}

export function EditOrganizationDialog({
  organization,
}: EditOrganizationDialogProps) {
  const [open, setOpen] = useState(false);

  const handleSuccess = () => {
    setOpen(false);
  };

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger render={<Button />}>Edit Organization</DialogTrigger>

      <DialogContent className="sm:max-w-xl">
        <DialogHeader>
          <DialogTitle>Edit Organization</DialogTitle>

          <DialogDescription>
            Update your organization basic information.
          </DialogDescription>
        </DialogHeader>

        <EditOrganizationForm
          organization={organization}
          onSuccess={handleSuccess}
        />
      </DialogContent>
    </Dialog>
  );
}
