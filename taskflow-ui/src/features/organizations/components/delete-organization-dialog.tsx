"use client";

import { useState } from "react";
import { Trash2 } from "lucide-react";

import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog";

import { Button } from "@/components/ui/button";

import { useDeleteOrganization } from "../hooks/use-delete-organization";

interface DeleteOrganizationDialogProps {
  organizationId: string;
  organizationName: string;
  onSuccess?: () => void;
}

export function DeleteOrganizationDialog({
  organizationId,
  organizationName,
  onSuccess,
}: DeleteOrganizationDialogProps) {
  const [open, setOpen] = useState(false);

  const deleteOrganization = useDeleteOrganization();

  const handleDelete = () => {
    deleteOrganization.mutate(organizationId, {
      onSuccess: () => {
        setOpen(false);
        onSuccess?.();
      },
    });
  };

  return (
    <AlertDialog open={open} onOpenChange={setOpen}>
      <AlertDialogTrigger render={<Button />}>
        Delete Organization
      </AlertDialogTrigger>

      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>Delete Organization?</AlertDialogTitle>

          <AlertDialogDescription>
            Are you sure you want to delete{" "}
            <span className="font-medium text-foreground">
              {organizationName}
            </span>
            ? This action cannot be undone.
          </AlertDialogDescription>
        </AlertDialogHeader>

        <AlertDialogFooter>
          <AlertDialogCancel disabled={deleteOrganization.isPending}>
            Cancel
          </AlertDialogCancel>

          <AlertDialogAction
            onClick={(event) => {
              event.preventDefault();
              handleDelete();
            }}
            disabled={deleteOrganization.isPending}
            className="bg-destructive text-destructive-foreground hover:bg-destructive/90"
          >
            {deleteOrganization.isPending
              ? "Deleting..."
              : "Delete Organization"}
          </AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  );
}
